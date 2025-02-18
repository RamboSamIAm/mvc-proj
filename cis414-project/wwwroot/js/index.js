$(document).ready(function () {
    $(document).on('click', '.home-playlist-item-x', function () {
        var playlistId = $(this).data('playlist-id');

        $.ajax({
            url: '/Playlist/DeletePlaylist',
            type: 'POST',
            data: { playlistId: playlistId }
        });
        window.location.reload();
    });
});
