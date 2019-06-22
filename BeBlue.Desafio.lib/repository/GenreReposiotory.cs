using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using Dapper;
using MySql.Data.MySqlClient;

namespace BeBlue.Desafio.lib.repository {
    public class GenreReposiotory : BaseRepository, IGenreRepository {
        public GenreReposiotory (string connetionString) : base (connetionString) { }

        public async Task<IEnumerable<Genre>> GetAsync () {
            IEnumerable<Genre> items;

            var query = "SELECT id, name FROM genres;";

            using (var conn = new MySqlConnection (_connetionString)) {
                items = (await conn.QueryAsync<Genre> (query)).ToList ();
            }

            return items;

        }

        public async Task<Genre> GetByNameAsync (string name) {
            var query = $"SELECT id, name FROM genres WHERE name = '{name}';";

            using (var conn = new MySqlConnection (_connetionString)) {

                var item = await conn.QueryFirstOrDefaultAsync<Genre> (query.ToString ());
                return item;
            }

        }
    }
}