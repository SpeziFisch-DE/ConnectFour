namespace ConnectFour.Objects {
    class RegisteredUser {
        public string username  {get;set;}
        public string password  {get;set;}
        public int statsLink  {get;set;}


        public RegisteredUser (string username, string password, int statsLink) {
            this.username = username;
            this.password = password;
            this.statsLink = statsLink;
        }
        public RegisteredUser (string username, string password) {
            this.username = username;
            this.password = password;
        }
        public RegisteredUser () {
            this.username = "";
            this.password = "";
        }
    }
}