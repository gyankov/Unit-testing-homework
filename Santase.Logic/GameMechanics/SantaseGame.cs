﻿namespace Santase.Logic.GameMechanics
{
    using Santase.Logic.Logger;
    using Santase.Logic.Players;
    using Santase.Logic.WinnerLogic;

    public class SantaseGame : ISantaseGame
    {
        private readonly IGameRules gameRules;

        private readonly IPlayer firstPlayer;

        private readonly IPlayer secondPlayer;

        private readonly ILogger logger;

        private PlayerPosition firstToPlay;

        private int secondPlayerTotalPoints;

        private int firstPlayerTotalPoints;

        public SantaseGame(
            IPlayer firstPlayer,
            IPlayer secondPlayer,
            PlayerPosition firstToPlay = PlayerPosition.FirstPlayer)
            : this(firstPlayer, secondPlayer, firstToPlay, GameRulesProvider.Santase, new NoLogger())
        {
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public SantaseGame(IPlayer firstPlayer, IPlayer secondPlayer, PlayerPosition firstToPlay, IGameRules gameRules, ILogger logger)
        {
            this.firstPlayerTotalPoints = 0;
            this.secondPlayerTotalPoints = 0;
            this.RoundsPlayed = 0;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.firstToPlay = firstToPlay;
            this.gameRules = gameRules;
            this.logger = logger;
        }

        public int FirstPlayerTotalPoints => this.firstPlayerTotalPoints;

        public int SecondPlayerTotalPoints => this.secondPlayerTotalPoints;

        public int RoundsPlayed { get; private set; }

        public PlayerPosition Start()
        {
            this.firstPlayer.StartGame(this.secondPlayer.Name);
            this.secondPlayer.StartGame(this.firstPlayer.Name);

            while (this.GameWinner() == PlayerPosition.NoOne)
            {
                this.PlayRound();
                this.RoundsPlayed++;
            }

            var gameWinner = this.GameWinner();

            this.firstPlayer.EndGame(gameWinner == PlayerPosition.FirstPlayer);
            this.secondPlayer.EndGame(gameWinner == PlayerPosition.SecondPlayer);

            return gameWinner;
        }

        private void PlayRound()
        {
            var round = this.firstToPlay == PlayerPosition.FirstPlayer
                            ? new Round(this.firstPlayer, this.secondPlayer, this.gameRules)
                            : new Round(this.secondPlayer, this.firstPlayer, this.gameRules);

            var roundResult = round.Play();

            this.logger.LogLine(
                this.firstToPlay == PlayerPosition.FirstPlayer
                    ? $"{roundResult.FirstPlayer.RoundPoints} - {roundResult.SecondPlayer.RoundPoints}"
                    : $"{roundResult.SecondPlayer.RoundPoints} - {roundResult.FirstPlayer.RoundPoints}");

            this.UpdatePoints(roundResult);
        }

        private void UpdatePoints(RoundResult roundResult)
        {
            IRoundWinnerPointsLogic roundWinnerPointsPointsLogic = new RoundWinnerPointsPointsLogic();
            var roundWinnerPoints = roundWinnerPointsPointsLogic.GetWinnerPoints(
                roundResult.FirstPlayer.RoundPoints,
                roundResult.SecondPlayer.RoundPoints,
                roundResult.GameClosedBy,
                roundResult.NoTricksPlayer,
                this.gameRules);

            if (roundWinnerPoints.Winner == PlayerPosition.NoOne)
            {
                return;
            }

            if (this.firstToPlay == PlayerPosition.FirstPlayer)
            {
                if (roundWinnerPoints.Winner == PlayerPosition.FirstPlayer)
                {
                    this.firstPlayerTotalPoints += roundWinnerPoints.Points;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                }
                else
                {
                    this.secondPlayerTotalPoints += roundWinnerPoints.Points;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                }
            }
            else
            {
                if (roundWinnerPoints.Winner == PlayerPosition.FirstPlayer)
                {
                    // It is actually our second player
                    this.secondPlayerTotalPoints += roundWinnerPoints.Points;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                }
                else
                {
                    this.firstPlayerTotalPoints += roundWinnerPoints.Points;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                }
            }
        }

        private PlayerPosition GameWinner()
        {
            if (this.FirstPlayerTotalPoints >= this.gameRules.GamePointsNeededForWin)
            {
                return PlayerPosition.FirstPlayer;
            }

            if (this.SecondPlayerTotalPoints >= this.gameRules.GamePointsNeededForWin)
            {
                return PlayerPosition.SecondPlayer;
            }

            return PlayerPosition.NoOne;
        }
    }
}
