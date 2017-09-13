using System;

namespace configCheck
{
    class config
    {
        private string playerName;
        private TimeZone localZone;
        private string scalaVersion;
        private bool solarWinds, MSP, teamviewer;

        public config()
        {

        }

        public string getPlayerName() { return this.playerName; }
        public void setPlayerName(string name) { this.playerName = name; }

        public TimeZone getTimeZone() { return this.localZone; }
        public void setTimeZone(TimeZone zone) { this.localZone = zone; }

        public string getScalaVersion() { return this.scalaVersion; }
        public void setScalaVersion(string version) { this.scalaVersion = version; }

        public bool getSolarWinds() { return this.solarWinds; }
        public void setSolarWinds(bool SW) { this.solarWinds = SW; }

        public bool getMSP() { return this.MSP; }
        public void setMSP(bool remote) { this.MSP = remote; }

        public bool getTeamViewer() { return this.teamviewer; }
        public void setTeamViewer(bool TV) { this.teamviewer = TV; }
    }
}
