﻿using System;

namespace MORF.Solution
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            switch (s)
            {
                case null:
                case "":
                    return true;
                default:
                    return false;
            }
        }
    }
}
