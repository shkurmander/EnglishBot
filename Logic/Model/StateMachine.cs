using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class StateMachine
    {
        public enum State {Active, Inactive, EnglishWord, Translation, Category }

        private State currentState { get; set; }

        public void SetState(string state)
        {            
            switch (state)
            {
                case "EnglishWord":
                    currentState = State.EnglishWord;
                    break;
                case "Translation":
                    currentState = State.Translation;
                    break;
                case "Category":
                    currentState = State.Category;
                    break;
                case "Inactive":
                    currentState = State.Inactive;
                    break;
                case "Active":
                    currentState = State.Active;
                    break;
            }
            // логика по-умолчанию для установленного состояния( еще хз пригодится или нет)
            //switch (currentState)
            //{
            //    case State.Inactive:
            //        break;
            //    case State.EnglishWord:
            //        break;
            //    case State.Translation:
            //        break;
            //    case State.Category:
            //        break;
            //}
        }
        public StateMachine()
        {
            currentState = State.Inactive;
        }

        public string GetState()
        {
            return currentState.ToString();
        }

    }
}
