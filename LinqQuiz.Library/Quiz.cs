﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
                throw new ArgumentOutOfRangeException();

            List<int> numbers = new List<int>();

            for (int i = 1; i < exclusiveUpperLimit; i++)
                numbers.Add(i);

            var retList = numbers.Where(n => n % 2 == 0);

            return retList.ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
                return new int[0];

            List<int> numbers = new List<int>();

            for (int i = exclusiveUpperLimit - 1; i > 0; i--)
                numbers.Add(checked (i*i));

            var retList = numbers.Where(n => n % 7 == 0);

            return retList.ToArray();
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if (families == null)
                throw new ArgumentNullException();

            List<FamilySummary> summaries = new List<FamilySummary>();

            foreach (var current in families)
            {
                FamilySummary newSummary = new FamilySummary();
                newSummary.FamilyID = current.ID;
                newSummary.NumberOfFamilyMembers = current.Persons.Count;

                Decimal sum = 0;
                foreach (var person in current.Persons)
                    sum = sum + person.Age;
                sum = sum / current.Persons.Count;

                newSummary.AverageAge = sum;

                summaries.Add(newSummary);
            }

            return summaries.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            char[] textArr = text.ToUpper().ToCharArray();
            (char letter, int numberOfOccurences)[] arr = new(char letter, int numberOfOccurences)[26];

            char currChar = 'A';
            for (int i = 0; i < arr.Length; i++)
            {
                var chars = textArr.Where(c => c.Equals(currChar));
                arr[i] = (currChar, chars.Count());
                currChar++;
            }

            var ret = arr.Where(e => e.numberOfOccurences != 0);

            return ret.ToArray();
        }
    }
}

