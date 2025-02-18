$(document).ready(function () {
    var playlistId = $('.playlist-container').data('playlist-id');
    
    $('.playlist-song-add-button[data-song-id]').click(function () {
        var songId = $(this).data('song-id');

        $.ajax({
            url: '/Playlist/AddSongToPlaylist', 
            type: 'POST',
            data: { songId: songId, playlistId: playlistId }
        });
        window.location.reload();
    });

    $('.playlist-song-remove-button[data-song-id]').click(function () {
        var songId = $(this).data('song-id');

        $.ajax({
            url: '/Playlist/RemoveSongFromPlaylist',  
            type: 'POST',
            data: { songId: songId, playlistId: playlistId }
        });
        window.location.reload();
    });

    $('.updatePlaylistNameButton').click(function () {
        var newPlaylistName = $('#newPlaylistName').val();

        if (newPlaylistName) {
            $.ajax({
                url: '/Playlist/UpdatePlaylistName',  
                type: 'POST',
                data: { playlistId: playlistId, playlistName: newPlaylistName }
            });

            setTimeout(() => {
                window.location.reload();
            }, 1000);
        } else {
            alert('Please enter a valid playlist name.');
        }
    });
});
