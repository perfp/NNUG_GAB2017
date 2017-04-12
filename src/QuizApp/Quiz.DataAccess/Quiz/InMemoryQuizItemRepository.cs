﻿using System.Collections.Generic;
using System.Linq;

namespace Quiz.DataAccess.Quiz
{
    public sealed class InMemoryQuizItemRepository : IQuizItemRepository
    {
        private static volatile InMemoryQuizItemRepository _instance;
        private static readonly object SyncRoot = new object();

        private InMemoryQuizItemRepository()
        {
            Items = new List<QuizItem>();
        }

        public static InMemoryQuizItemRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InMemoryQuizItemRepository();
                    }
                }

                return _instance;
            }
        }

        public static List<QuizItem> Items { get; set; }

        public IEnumerable<QuizItem> GetAll()
        {
            return Items;
        }

        public QuizItem Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public void Add(QuizItem model)
        {
            model.Id = GetNextId();
            Items.Add(model);
        }

        public void Update(QuizItem model)
        {
            Delete(model.Id);

            Items.Add(model);
        }

        public void Delete(int id)
        {
            var existingItem = Get(id);
            if (existingItem == null)
                return;

            Items.Remove(existingItem);
        }

        private int GetNextId()
        {
            return GetAll().Any() ? GetAll().Max(o => o.Id) + 1 : 1;
        }
    }
}