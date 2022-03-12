﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WannaWhat
{
    public static class Extensions
    {
        private static List<String> PersonalInfoList { get { return new List<String> { { "Name" }, { "family_name" }, { "dob" }, { "age" } }; } }
        private static List<String> PersonalityList { get { return new List<String> { { "shy" }, { "quiet" }, { "friednly" }, { "rude" }, { "outSpoken" }, { "arrogant" }, { "confident" }, { "loud" }, { "introvert" }, { "extrovert" }}; } }

        private static List<String> InterestsList { get { return new List<String> { { "boys" }, { "girls" }, { "indoors" }, { "outdoors" }, { "sports" }, { "boardGames" }, { "videoGames" } }; } }

       // private static List<String> HobbiesList { get { return new List<String> { { "boys" }, { "girls" }, { "indoors" }, { "outdoors" }, { "sports" }, { "boardGames" }, { "videoGames" } }; } }



        public static IEnumerable<Claim> GetPersonInfo(this IEnumerable<Claim> list)
        {
            List<Claim> result = new List<Claim>();
            Claim c;
            foreach (string claim in Extensions.PersonalInfoList)
            {
                c = list.FirstOrDefault(x => x.Type.Equals(claim, StringComparison.OrdinalIgnoreCase));
                if (c != null)
                    result.Add(c);
            }

            return result;
        }

        public static IEnumerable<Claim> GetPersonalityInfo(this IEnumerable<Claim> list)
        {
            List<Claim> result = new List<Claim>();
            Claim c;
            foreach (string claim in Extensions.PersonalityList)
            {
                c = list.FirstOrDefault(x => x.Type.Equals(claim, StringComparison.OrdinalIgnoreCase));
                if (c != null)
                    result.Add(c);
            }

            return result;
        }

        public static IEnumerable<Claim> GetInterestInfo(this IEnumerable<Claim> list)
        {
            List<Claim> result = new List<Claim>();
            Claim c;
            foreach (string claim in Extensions.InterestsList)
            {
                c = list.FirstOrDefault(x => x.Type.Equals(claim, StringComparison.OrdinalIgnoreCase));
                if (c != null)
                    result.Add(c);
            }

            return result;
        }
    }
}
