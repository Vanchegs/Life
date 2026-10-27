using System;
using UnityEngine;

namespace Codebase
{
    public class GameEventBus : MonoBehaviour
    {
        public static event Action<int> OnUpdateBalance;
        public static event Action<int> OnUpdateCasinoBalance;
        public static event Action<int> OnUpdateBetValueChange;
        public static event Action<int> OnUpdateDayNumber;
        public static event Action<LossGameTypes> OnLossGame;
        public static event Action OnSaveGame;
        public static event Action OnFellMental;
        public static event Action OnFellHealth;
        public static event Action OnFellHungry;
        public static event Action OnFellEnergy;
        public static event Action<StatType> OnStatRecovered;

        public static int Balance { get; private set; }
        public static int CasinoBalance { get; private set; }
        public static int BetValue { get; private set; } = 10;
        public static int DayNumber { get; private set; } = 1;

        public static void RaiseUpdateBalance(int balance)
        {
            Balance = balance;
            OnUpdateBalance?.Invoke(balance);
        }

        public static void RaiseUpdateCasinoBalance(int balance)
        {
            CasinoBalance = balance;
            OnUpdateCasinoBalance?.Invoke(balance);
        }

        public static void RaiseUpdateBetValueChange(int betValue)
        {
            BetValue = betValue;
            OnUpdateBetValueChange?.Invoke(betValue);
        }

        public static void RaiseUpdateDayNumber(int dayNumber)
        {
            DayNumber = dayNumber;
            OnUpdateDayNumber?.Invoke(dayNumber);
        }

        public static void RaiseLossGame(LossGameTypes lossType) =>
            OnLossGame?.Invoke(lossType);

        public static void RaiseSaveGame() =>
            OnSaveGame?.Invoke();

        public static void RaiseFellMental() =>
            OnFellMental?.Invoke();

        public static void RaiseFellHealth() =>
            OnFellHealth?.Invoke();

        public static void RaiseFellHungry() =>
            OnFellHungry?.Invoke();

        public static void RaiseFellEnergy() =>
            OnFellEnergy?.Invoke();

        public static void RaiseStatRecovered(StatType statType) =>
            OnStatRecovered?.Invoke(statType);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetEvents()
        {
            OnUpdateBalance = null;
            OnUpdateCasinoBalance = null;
            OnUpdateBetValueChange = null;
            OnUpdateDayNumber = null;
            OnLossGame = null;
            OnSaveGame = null;
            OnFellMental = null;
            OnFellHealth = null;
            OnFellHungry = null;
            OnFellEnergy = null;
            OnStatRecovered = null;

            Balance = 0;
            CasinoBalance = 0;
            BetValue = 10;
            DayNumber = 1;
        }
    }
}
