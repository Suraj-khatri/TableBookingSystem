using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Restro.DTO;
using Restro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.AppServices
{
    public class AppServices : RestroAppServiceBase
    {
        private readonly IRepository<Tables > _tableRepository;
        private readonly IRepository<Customers > _customersRepository;
        private readonly IRepository<Reservations > _reservationsRepository;

        public AppServices(IRepository<Tables> tableRepository,
            IRepository<Customers> customersRepository,
            IRepository<Reservations> reservationsRepository
            )
        {
            _tableRepository = tableRepository;
            _customersRepository = customersRepository;
            _reservationsRepository = reservationsRepository;
        }

        public async Task CreateTable(CreateTableDTO input)
        {
            var table = new Tables
            {
                TableNo = input.TableNo,
                Capacity = input.Capacity,
                IsAvailable = input.IsAvailable,
            };
            await _tableRepository.InsertAsync(table);
        }

        public async Task<List<GetViewForAllTablesDTO>>GetAllTables()
        {
            var tables = await _tableRepository.GetAll().AsNoTracking()
                .Select(x=>new GetViewForAllTablesDTO
                {
                    Id = x.Id,
                    TableNo =x.TableNo,
                    Capacity=x.Capacity,

                }).ToListAsync();
            if(!tables.Any())
            {
                throw new UserFriendlyException("No Tables Found");
            }
            return tables;
        }

        public async Task<List<GetViewForAllTablesDTO>>GetAllAvailableTables()
        {
            var tables = await _tableRepository.GetAll().AsNoTracking()
                .Where(x=>x.IsAvailable == true)
                .Select(x=>new GetViewForAllTablesDTO
                {   
                    Id = x.Id,
                    TableNo=x.TableNo,
                    Capacity=x.Capacity,
                }).ToListAsync();
            if (!tables.Any())
            {
                throw new UserFriendlyException("No Available Tables Found");
            }
            return tables;
        }

        public async Task<ViewForTableDTO>GetDetailsOfTable(int tableNo)
        {
            var table = await _tableRepository.GetAll().AsNoTracking()
                .Where(x=>x.TableNo == tableNo)
                .Select(x=> new  ViewForTableDTO
                {
                    IsAvailable = x.IsAvailable
                }).FirstOrDefaultAsync();
            if (table == null)
            {
                throw new UserFriendlyException("No Table Found");
            }
            return table;
                
        }

        public async Task UpdateTable(CreateTableDTO input)
        {

            var table = await _tableRepository.FirstOrDefaultAsync(x => x.TableNo == input.TableNo);
            if (table == null)
            {
                throw new UserFriendlyException("No Such Tables With This Table Number");
            }
            table.Capacity = input.Capacity;
            table.IsAvailable = input.IsAvailable;
            await _tableRepository.UpdateAsync(table);

        }

        public async Task Delete(int tableNo)
        {
            var table = await _tableRepository.FirstOrDefaultAsync(x=>x.TableNo == tableNo);
            if (table == null)
            {
                throw new UserFriendlyException("No Such Tables With This Id");
            }
            await _tableRepository.DeleteAsync(table);
        }

        public async Task<int> GetTotalNumberOfTable()
        {
            var num = await _tableRepository.CountAsync();
            
            if(num > 0)
            {
                return num;
            }
            return 0;
        }

        public async Task CreateReservaton(CreateReservationDTO input)
        {
            
            var table = await _tableRepository.GetAll().AsNoTracking()
                .Where(x=>x.TableNo == input.TableNo)
                .FirstOrDefaultAsync();
            //if party size is more than capacity
            if (input.PartySize > table.Capacity)
            {
                throw new UserFriendlyException("Party Size Exceeds Table Capacity");
            }
            //if table is not available
            if(!table.IsAvailable)
            {
                throw new UserFriendlyException("table is not available");
            }
            var customer = new Customers
            {
                CustomerName = input.Customers.Name.ToLower(),
                CustomerEmail = input.Customers.Email,
            };
            var customerId = await _customersRepository.InsertAndGetIdAsync(customer);
            int tableId = await _tableRepository.GetAll().AsNoTracking()
                .Where(x=>x.TableNo == input.TableNo)
                .Select(x => x.Id).FirstOrDefaultAsync();

            var res = new Reservations
            {
                CustomerId = customerId,
                TableId = table.Id,
                PartySize = input.PartySize,
                ReservationTime = DateTime.Today
            };

            //when table is reserved, we need to make isavailable false
            table.IsAvailable = false;
           // table.Capacity = table.Capacity;
            //table.TableNo = table.TableNo;
            await _tableRepository.UpdateAsync(table);
            await _reservationsRepository.InsertAsync(res);
        }

        public async Task<List<GetViewForAllReservationsDTO>>GetAllReservations()
        {
            var allReservations = await _reservationsRepository.GetAll().AsNoTracking()
                .ToListAsync();
            var results = new List<GetViewForAllReservationsDTO>();
            foreach (var reservation in allReservations)
            {
                var customer = await _customersRepository.FirstOrDefaultAsync(x => x.Id == reservation.CustomerId);
                var tables = await _tableRepository.FirstOrDefaultAsync(x => x.Id == reservation.TableId);

                var result = new GetViewForAllReservationsDTO
                {
                    Id = reservation.Id,
                    CustomerName = customer.CustomerName,
                    Email = customer.CustomerEmail,
                    PartySize = reservation.PartySize,
                    TableNo = tables.TableNo
                };
                results.Add(result);
            }

            return results;
        }

        public async Task<List<GetViewForSpecificReservationDTO>>GetReservation(string  customerName)
        {
            var reservations = await _reservationsRepository.GetAll().AsNoTracking()
                .Include(x => x.CustomerFK)
                .Where(x => x.CustomerFK.CustomerName.Contains(customerName))
                .ToListAsync();
            if(!reservations.Any())
            {
                throw new UserFriendlyException("No Reservations Found");
            }
            var results = new List<GetViewForSpecificReservationDTO>();

            foreach(var reservation in reservations)
            {
                var customers = await _customersRepository.GetAll().AsNoTracking()
                    .Where(x => x.Id == reservation.CustomerId)
                    .FirstOrDefaultAsync();
                int tableNo = await _tableRepository.GetAll().AsNoTracking()
                .Where(x => x.Id == reservation.TableId)
                .Select(x => x.TableNo).FirstOrDefaultAsync();
                var result = new GetViewForSpecificReservationDTO
                {
                   CustomerName = customers.CustomerName,
                    CustomerId = reservation.CustomerId,
                   PartySize = reservation.PartySize,
                    ReservationId = reservation.Id,
                    TableNo = tableNo

                };
                results.Add(result) ;
            }
            return results;
        }

        public async Task ReservationsUpdate(UpdateReservationDTO input)
        {
            var reservationDetails = await _reservationsRepository.FirstOrDefaultAsync(x => x.Id == input.ReservationId);
            var table = await _tableRepository.FirstOrDefaultAsync(x => x.Id == reservationDetails.TableId);
            var customer = await _customersRepository.FirstOrDefaultAsync(x => x.Id == reservationDetails.CustomerId);

            if(input.Status == ReservationStatus.Cancelled)
            {
                table.IsAvailable = true;
                await _tableRepository.UpdateAsync(table);
                await _reservationsRepository.DeleteAsync(reservationDetails);
                await _customersRepository.DeleteAsync(customer);
               
            }
            if(input.Status == ReservationStatus.Confirmed)
            {
                if(input.PartySize > table.Capacity)
                {
                    throw new UserFriendlyException("Party Size Exceeds Table Capacity");
                }
                reservationDetails.PartySize = input.PartySize;
                await _tableRepository.UpdateAsync(table);
            }
            
        }

        // using IQueryable
        public async Task<ViewForTableDTO> GetDetailsOfTables(int tableNo)
        {
            var tables = _tableRepository.GetAll().AsNoTracking();
            var table = tables.Where(x => x.TableNo == tableNo)
                .Select(x => new ViewForTableDTO
                {
                    IsAvailable = x.IsAvailable,
                }).FirstOrDefault();
            if (table == null)
            {
                throw new UserFriendlyException("No Table Found");
            }
            return table;

        }

        public async Task DeleteReservation(int reservationId)
        {
            var reservationDetails = await _reservationsRepository.FirstOrDefaultAsync(x=>x.Id == reservationId);
            if (reservationDetails == null)
            {
                throw new UserFriendlyException("Reservation Not Found");
            }
            var CustomerDetails = await _customersRepository.FirstOrDefaultAsync(x=>x.Id==reservationDetails.CustomerId);
            var tableDetails = await _tableRepository.FirstOrDefaultAsync(x=>x.Id==reservationDetails.TableId);
            tableDetails.IsAvailable = true;
            await _reservationsRepository.DeleteAsync(reservationDetails);
            await _customersRepository.DeleteAsync(CustomerDetails);
            await _tableRepository.UpdateAsync(tableDetails);
        }
    }
}
