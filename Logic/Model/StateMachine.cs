using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class StateMachine
    {

        public enum State
        {
            Active, Inactive, 
            EnglishWord, Translation, Category,
            TrainingEn, TrainingRu
        }


        private TrainingConfig config;
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
                case "TrainingEn":
                    currentState = State.TrainingEn;
                    break;
                case "TrainingRu":
                    currentState = State.TrainingRu;
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
        /// <summary>
        /// Устанавливает конфиг тренировки
        /// </summary>
        /// <param name="isActive"> True - тренировка активна</param>
        /// <param name="isThematic">True - тренировка по тематикам, false - все слова</param>
        /// <param name="direction">True - перевод с русского на английский, false - с английского на русский</param>
        public void SetTraningConfig(TrainingConfig options)
            => config = options;

        /// <summary>
        /// Возвращает конфиг тренировки
        /// </summary>
        /// <returns></returns>
        public TrainingConfig GetTraningConfig() => config;


    }
}
