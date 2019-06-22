using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using Dapper;
using MySql.Data.MySqlClient;

namespace BeBlue.Desafio.lib.repository {
    public class SaleRepository : BaseRepository, ISaleRepository {
        public SaleRepository (string connetionString) : base (connetionString) { }

        public async Task<IEnumerable<Sale>> GetAsync () {
            var query = "SELECT id, dateSale, total, totalCashback FROM sales;";

            using (var conn = new MySqlConnection (_connetionString)) {
                var items = (await conn.QueryAsync<Sale> (query)).ToList ();
                return items;
            }
        }
        public async Task<IEnumerable<Sale>> GetByDateAsync (DateTime begin, DateTime end, int PageSize = 50, int PageNumber = 1) {
            int start = @PageSize * (@PageNumber - 1);
            var query = $@"SELECT 
                            id, 
                            dateSale, 
                            total, 
                            totalCashback 
                        FROM sales 
                        WHERE 
                            dateSale >= '{begin.ToString("yyyy-MM-dd")}'
                            AND dateSale < '{end.AddDays(1).ToString("yyyy-MM-dd")}'
                        ORDER BY dateSale DESC
                        LIMIT {start}, {@PageSize};";

            using (var conn = new MySqlConnection (_connetionString)) {
                var items = (await conn.QueryAsync<Sale> (query)).ToList ();
                return items;
            }
        }

        public int Create (Sale sale) {
            var query = @"INSERT INTO sales (dateSale, total, totalCashback) 
                            VALUES (@DateSale, @Total, @TotalCashback);
                            SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);";;
            var result = 0;
            using (var conn = new MySqlConnection (_connetionString)) {
                result = conn.Query<int> (query, sale, commandTimeout : 120).Single ();
            }

            return result;
        }
  

        public async Task<bool> UpdateAsync (Sale sale) {
            var query = @"UPDATE sales SET total=@Total,  
                             totalCashback=@TotalCashback
                             WHERE id=@Id;";
            using (var conn = new MySqlConnection (_connetionString)) {
                _ = await conn.QueryAsync (query, sale, commandTimeout : 120);
            }

            return true;
        }

        public async Task<Sale> GetAsync (int idSale) {

            var query = $"SELECT id, dateSale, total, totalCashback FROM sales where id = {idSale};";

            using (var conn = new MySqlConnection (_connetionString)) {
                var item = await conn.QueryFirstOrDefaultAsync<Sale> (query.ToString ());
                return item;
            }

        }
    }
}