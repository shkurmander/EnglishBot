using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class StateMachine
    {
        /// <summary>
        /// Состояния активных диалогов
        /// </summary>
        public enum State
        {
            Inactive,
            EnglishWord, Translation, Category,
            TrainingEn, TrainingRu
        }
        /// <summary>
        /// Типы диалогов
        /// </summary>
        public enum MainStates
        {
            Inactive, AddWordDialog, TrainingDialog
            
        }

        private TrainingConfig config;
        /// <summary>
        /// текущая запись для тренировки из словаря 
        /// </summary>
        private WordRecord tempWord;

        /// <summary>
        /// Внутреннее состояние активного диалога
        /// </summary>
        private State currentState;
        /// <summary>
        /// Тип текущего диалога
        /// </summary>
        private MainStates mainState;


        /// <summary>
        /// Устанавливает внутреннее состояние активного диалога
        /// </summary>
        /// <param name="state"></param>
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
                case "AddWordDialog":
                    mainState = MainStates.AddWordDialog;
                    break;
                case "TrainingDialog":
                    mainState = MainStates.TrainingDialog;
                    break;
                case "Inactive":
                    currentState = State.Inactive;                    
                    break;
                case "TrainingEn":
                    currentState = State.TrainingEn;
                    break;
                case "TrainingRu":
                    currentState = State.TrainingRu;
                    break;
            }
            
        }
        /// <summary>
        /// Останавливает текущий диалог
        /// </summary>
        
        public void StopDialog ()
        {
            mainState = MainStates.Inactive;
            currentState = State.Inactive;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public StateMachine()
        {
            mainState = MainStates.Inactive;
            currentState = State.Inactive;
        }

        /// <summary>
        /// Возвращает тип текущего диалога
        /// </summary>
        /// <returns></returns>
        public string GetMainState()
        {
            return mainState.ToString();
        }

        /// <summary>
        /// Возвращает внутреннее состояние активного диалога
        /// </summary>
        /// <returns></returns>
        public string GetCurrentState()
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
        /// <summary>
        /// Возвращает тренировочное слово
        /// </summary>
        /// <returns></returns>
        public WordRecord GetTempWord() => tempWord;
        /// <summary>
        /// Устанавливает тренировочное слово
        /// </summary>
        /// <param name="word"></param>       

        public void SetTempWord(WordRecord word) { tempWord = word; }

        //хз возможно убрать
        public void NextStep() { config.NextStep(); }
        


    }
}
