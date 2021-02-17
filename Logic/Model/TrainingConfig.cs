using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class TrainingConfig
    {
        private bool trainingIsActive = false;
        private bool trainingThematic = false;
        private bool trainingRuToEnglish = false;
        private string thematic = "";

        public TrainingConfig(bool isActive, bool isThematic, bool direction, string thematic)
        {
            trainingIsActive = isActive;
            trainingThematic = isThematic;
            trainingRuToEnglish = direction;
            this.thematic = thematic;
        }
        public TrainingConfig()
        {

        }
        public bool IsRuToEN()
        {
            return trainingRuToEnglish;
        }

        public bool IsThematic()
        {
            return trainingThematic;
        }

        public bool IsActive()
        {
            return trainingIsActive;
        }
        public string GetThematic() 
        { 
            return thematic; 
        }
	

	}


    
}
