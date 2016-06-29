﻿using System;

namespace PVDevelop.UCoach.Server.Timing
{
    public static class  UtcTime
    {
        private static DateTime? _utcNow;

        public static void SetUtcNow()
        {
            _utcNow = DateTime.UtcNow;
        }

        public static DateTime UtcNow
        {
            get
            {
                return _utcNow ?? DateTime.UtcNow;
            }
        }
    }
}