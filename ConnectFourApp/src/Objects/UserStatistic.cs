namespace ConnectFour.Objects {
    class UserStats {
        public int timesPlayed  {get;set;}
        public int timesWon  {get;set;}
        public int timesLost  {get;set;}

        public UserStats (int timesPlayed, int timesWon, int timesLost) {
            this.timesPlayed = timesPlayed;
            this.timesWon = timesWon;
            this.timesLost = timesLost;
        }
        public UserStats () {
            this.timesPlayed = 0;
            this.timesWon = 0;
            this.timesLost = 0;
        }
    }
}