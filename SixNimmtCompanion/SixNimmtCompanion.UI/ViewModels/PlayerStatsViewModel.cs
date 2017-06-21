﻿using SixNimmtCompanion.UI.Models;

namespace SixNimmtCompanion.UI.ViewModels
{
    public class PlayerStatsViewModel : BaseViewModel
    {
        private PlayerStat _stats;
        public PlayerStat stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                OnPropertyChanged(nameof(stats));
            }
        }
    }
}
