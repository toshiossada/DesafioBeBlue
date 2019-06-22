using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using Dapper;
using MySql.Data.MySqlClient;

namespace BeBlue.Desafio.lib.repository {
    public class TrackRepository : BaseRepository, ITrackRepository {

        public TrackRepository (string connetionString) : base (connetionString) { }
        public async Task<IEnumerable<Track>> GetAsync () {
            var query = @"SELECT
                            T.id,
                            T.name,
                            T.idGenre,
                            T.price,
                            T.nameOfArtist,
                            G.name as genreName,
                            C.percent as percentCashback,
                            D.name as dayOfWeekName,
                            D.id idDayOfWeek
                        FROM tracks T
                        INNER JOIN genres G ON T.idGenre = G.Id
                        INNER JOIN cashback C ON G.id = C.idGenre
                        INNER JOIN dayOfWeek D ON C.idDayOfWeek = D.id;";

            using (var conn = new MySqlConnection (_connetionString)) {
                var items = (await conn.QueryAsync<Track> (query)).ToList ();
                return items;
            }
        }

        public int Create (Track track) {
            var query = @"INSERT INTO tracks (idGenre, name, nameOfArtist, price ) 
                            VALUES (@IdGenre, @Name, @NameOfArtist, @Price);
                            SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);";;
            var result = 0;
            using (var conn = new MySqlConnection (_connetionString)) {
                result = conn.Query<int> (query, track, commandTimeout : 120).Single ();
            }

            return result;
        }

        public async Task<bool> Delete (int idTrack) {
            var query = $"DELETE FROM tracks WHERE id = @IdTrack;";

            using (var conn = new MySqlConnection (_connetionString)) {

                _ = await conn.QueryAsync (query, new {
                    IdTrack = idTrack
                }, commandTimeout : 120);
            }

            return true;
        }

        public async Task<IEnumerable<Track>> GetBySaleAsync (int idSale) {
            var query = $@"SELECT
                            T.id,
                            T.idGenre,
                            T.price,
                            T.name,
                            T.nameOfArtist,
                            G.name as genreName,
                            s.cashback as percentCashback,
                            D.name as dayOfWeekName,
                            D.id idDayOfWeek
                        FROM tracks T
                        INNER JOIN saleItems s ON T.Id = s.idTrack
                        INNER JOIN genres G ON T.idGenre = G.Id
                        INNER JOIN dayOfWeek D ON s.idDayOfWeek = D.id
                        WHERE s.idSale = {idSale}";

            using (var conn = new MySqlConnection (_connetionString)) {
                var items = (await conn.QueryAsync<Track> (query)).ToList ();
                return items;
            }

            

        }

        public async Task<bool> DeleteAll () {
            var query = $"DELETE FROM tracks WHERE 1 = 1;";

            using (var conn = new MySqlConnection (_connetionString)) {

                _ = await conn.QueryAsync (query, null, commandTimeout : 120);
            }

            return true;
        }

        public async Task<Track> GetByIdAsync (int id) {
            var query = $@"SELECT
                            T.id,
                            T.idGenre,
                            T.price,
                            T.name,
                            T.nameOfArtist,
                            G.name as genreName,
                            C.percent as percentCashback,
                            D.name as dayOfWeekName
                        FROM tracks T
                        INNER JOIN genres G ON T.idGenre = G.Id
                        INNER JOIN cashback C ON G.id = C.idGenre
                        INNER JOIN dayOfWeek D ON C.idDayOfWeek = D.Id
                        WHERE T.id = {id}";

            using (var conn = new MySqlConnection (_connetionString)) {
                var item = await conn.QueryFirstOrDefaultAsync<Track> (query.ToString ());
                return item;
            }
        }

        public async Task<Track> GetByIdAndDayOfWeekAsync (int id, int? dayOfWeek) {
            var idDayOfWeek = (dayOfWeek == null) ? (int) DateTime.Today.DayOfWeek : dayOfWeek;

            var query = $@"SELECT
                            T.id,
                            T.idGenre,
                            T.price,
                            T.name,
                            T.nameOfArtist,
                            G.name as genreName,
                            C.percent as percentCashback,
                            D.name as dayOfWeekName
                        FROM tracks T
                        INNER JOIN genres G ON T.idGenre = G.Id
                        INNER JOIN cashback C ON G.id = C.idGenre
                        INNER JOIN dayOfWeek D ON C.idDayOfWeek = D.Id
                        WHERE T.id = {id} AND D.id = {idDayOfWeek}";

            using (var conn = new MySqlConnection (_connetionString)) {
                var item = await conn.QueryFirstOrDefaultAsync<Track> (query.ToString ());
                return item;
            }
        }

        public async Task<IEnumerable<Track>> GetByGenreAsync (string name, int PageSize = 50, int PageNumber = 1) {
            int start = @PageSize * (@PageNumber - 1);
            var query = $@"SELECT
                            T.id,
                            T.idGenre,
                            T.price,
                            T.name,
                            T.nameOfArtist
                        FROM tracks T
                        INNER JOIN genres G ON T.idGenre = G.id
                        WHERE G.name = '{name}'
                        ORDER BY T.name
                        LIMIT {start}, {@PageSize};";

            using (var conn = new MySqlConnection (_connetionString)) {

                var items = (await conn.QueryAsync<Track> (query)).ToList ();
                return items;
            }
        }
    }
}