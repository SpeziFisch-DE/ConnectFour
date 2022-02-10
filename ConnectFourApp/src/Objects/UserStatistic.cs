namespace ConnectFour.Objects {
    class UserStats {
        public int timesPlayed  {get;set;}
        public int timesWon  {get;set;}

        public UserStats (int timesPlayed, int timesWon) {
            this.timesPlayed = timesPlayed;
            this.timesWon = timesWon;
        }
        public UserStats () {
            this.timesPlayed = 0;
            this.timesWon = 0;
        }
    }
}