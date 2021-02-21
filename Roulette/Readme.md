This is the endpoint list:


1. Endpoint to add roulette: POST api/Roulette/AddRoulette
2. Endpoint to open roulette: PUT api/Roulette/OpenRoulette/{id}
3. Endpoint to add bet: POST api/Bet/AddBet
    this is a body example to add bet:
    {
        "betId": 1,
        "rouletteId": 1,
        "number": 37,
        "color": "Negro",
        "money": 5200
    }
    and the user id should be send by header with name 'UserId'
4. Endpoint to close bet: GET: api/Bet/CloseBets/{rouletteId}
5. Endpoint get all roulettes : GET: api/Roulette/GetAll