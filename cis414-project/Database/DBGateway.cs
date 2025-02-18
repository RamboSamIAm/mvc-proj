using System.Data;
using cis414_project.Models.Playlist;
using cis414_project.Models.Song;
using Microsoft.Data.SqlClient;

namespace cis414_project.Database
{
    public class DBGateway
    {
        private readonly string _connectionString;

        public DBGateway(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreatePlaylist(int userId, string playlistName)
        {
            try
            {
                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CreatePlaylist"
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@PlaylistName", playlistName);

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<PlaylistInList>> GetPlaylists(int userId)
        {
            try
            {
                var response = new List<PlaylistInList>();

                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetListOfPlaylists"
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    response.Add(new PlaylistInList
                    {
                        PlaylistId = (int)reader["PlaylistId"],
                        PlaylistName = reader["PlaylistName"].ToString(),
                        CreatedDate = (DateTime)reader["CreatedDate"]
                    });
                }

                return response;
            }
            catch (Exception e)
            {
                return new List<PlaylistInList>();
            }
        }

        public async Task DeletePlaylist(int playlistId)
        {
            try
            {
                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeletePlaylist"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playlistId);

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<SongItem>> GetSongsInPlaylist(int playListId)
        {
            try
            {
                var response = new List<SongItem>();

                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetSongsInPlaylist"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playListId);
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    response.Add(new SongItem
                    {
                        SongId = (int)reader["SongId"],
                        SongName = reader["SongName"].ToString(),
                        Artist = reader["Artist"].ToString(),
                        SongLength = reader["SongLength"].ToString()
                    });
                }

                return response;
            }
            catch (Exception e)
            {
                return new List<SongItem>();
            }
        }

        public async Task<List<SongItem>> GetAvailableSongs(int playListId)
        {
            try
            {
                var response = new List<SongItem>();

                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetSongsNotInPlaylist"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playListId);
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    response.Add(new SongItem
                    {
                        SongId = (int)reader["SongId"],
                        SongName = reader["SongName"].ToString(),
                        Artist = reader["Artist"].ToString(),
                        SongLength = reader["SongLength"].ToString()
                    });
                }

                return response;
            }
            catch (Exception e)
            {
                return new List<SongItem>();
            }
        }

        public async Task<BasicPlaylistInfo> GetBasicPlaylistInfo(int playlistId)
        {
            try
            {
                var response = new BasicPlaylistInfo();

                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetPlaylistInfo"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    response.PlaylistName = reader["PlaylistName"].ToString();
                    response.CreatedDate = (DateTime)reader["CreatedDate"];
                }

                return response;
            }
            catch (Exception e)
            {
                return new BasicPlaylistInfo();
            }
        }

        public async Task AddSongToPlaylist(int songId, int playlistId)
        {
            try
            {
                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddSongToPlaylist"
                };

                cmd.Parameters.AddWithValue("@PlaylistID", playlistId);
                cmd.Parameters.AddWithValue("@SongId", songId);

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemoveSongFromPlaylist(int songId, int playlistId)
        {
            try
            {
                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeleteSongFromPlaylist"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                cmd.Parameters.AddWithValue("@SongId", songId);

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdatePlaylistName(int playlistId, string playlistName)
        {
            try
            {
                await using var conn = new SqlConnection(this._connectionString);
                await using var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "EditPlaylistName"
                };

                cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                cmd.Parameters.AddWithValue("@NewPlaylistName", playlistName);

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
