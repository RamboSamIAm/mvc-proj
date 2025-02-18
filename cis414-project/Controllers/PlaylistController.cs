using cis414_project.Database;
using cis414_project.Models.Playlist;
using Microsoft.AspNetCore.Mvc;

namespace cis414_project.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly DBGateway _dbGateway;
        const int _userId = 1;

        public PlaylistController(DBGateway dbGateway)
        {
            _dbGateway = dbGateway;
        }

        public IActionResult AddPlaylist()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlaylist(string playlistName)
        {
            if (string.IsNullOrWhiteSpace(playlistName))
            {
                ViewBag.ErrorMessage = "Please enter a playlist name.";
                return View();  
            }

            await _dbGateway.CreatePlaylist(_userId, playlistName);

            return RedirectToAction("Index", "Home"); 
        }

        [HttpPost]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            try
            {
                await _dbGateway.DeletePlaylist(playlistId);
                return Json(new { success = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> YourPlaylist(int playlistId)
        {
            var model = new Playlist();
            var basicPlaylist = await _dbGateway.GetBasicPlaylistInfo(playlistId);

            model.PlaylistId = playlistId;
            model.PlaylistName = basicPlaylist.PlaylistName;
            model.CreatedDate = basicPlaylist.CreatedDate;
            model.SelectedSongs = await _dbGateway.GetSongsInPlaylist(playlistId);
            model.AvailableSongs = await _dbGateway.GetAvailableSongs(playlistId);

            return View(model);
        }

        [HttpPost]
        public async Task AddSongToPlaylist(int songId, int playlistId)
        {
            await _dbGateway.AddSongToPlaylist(songId, playlistId);
            await YourPlaylist(playlistId);
        }

        [HttpPost]
        public async Task RemoveSongFromPlaylist(int songId, int playlistId)
        {
            await _dbGateway.RemoveSongFromPlaylist(songId, playlistId);
            await YourPlaylist(playlistId);
        }

        [HttpPost]
        public async Task UpdatePlaylistName(int playlistId, string playlistName)
        {
            await _dbGateway.UpdatePlaylistName(playlistId, playlistName);
        }
    }
}
