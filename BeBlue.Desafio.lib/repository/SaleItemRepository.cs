using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using Dapper;
using MySql.Data.MySqlClient;

namespace BeBlue.Desafio.lib.repository {
    public class SaleItemRepository : BaseRepository, ISaleItemRepository {
        public SaleItemRepository (string connetionString) : base (connetionString) { }

        public int Create (SaleItem saleItem) {
            var query = @"INSERT INTO saleItems (idSale, idTrack, cashback, idDayOfWeek) 
                            VALUES (@IdSale, @IdTrack, @Cashback, @IdDayOfWeek);
                            SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);";;
            var result = 0;
            using (var conn = new MySqlConnection (_connetionString)) {
                result = conn.Query<int> (query, saleItem, commandTimeout : 120).Single ();
            }

            return result;
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleAsync (int idSale) {
            IEnumerable<SaleItem> items;

            var query = $"SELECT id, idSale, idTrack, cashback FROM saleItems where idSale = {idSale};";

            using (var conn = new MySqlConnection (_connetionString)) {
                items = (await conn.QueryAsync<SaleItem> (query)).ToList ();
            }

            return items;

        }

     
    }
}