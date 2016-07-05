var io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortId');

console.log('server started');

var players = [];

io.on('connection', function(socket) {

    var thisPlayerId = shortid.generate();

    players.push(thisPlayerId);

    console.log('client connected, broadcasting spawn, id:', thisPlayerId);

    socket.broadcast.emit('spawn', { id: thisPlayerId });

    players.forEach(function(playerId){
      if(playerId == thisPlayerId)
        return;
      socket.emit('spawn', {id: playerId});
      console.log("sending spawn to new player for id: ", playerId)
    });


    socket.on('move', function(data) {
        data.id = thisPlayerId;
        console.log('client moved', JSON.stringify(data));

        socket.broadcast.emit('move', data)
    });

    socket.on('disconnect', function() {
        console.log('client disconnected');

        players.splice(players.indexOf(thisPlayerId), 1);
        socket.broadcast.emit('disconnected', {id: thisPlayerId});
    })
});
