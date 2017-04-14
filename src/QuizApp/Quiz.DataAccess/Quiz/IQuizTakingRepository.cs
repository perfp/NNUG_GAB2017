using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public interface IQuizTakingRepository
    {
        IEnumerable<QuizTaking> GetAll();
        QuizTaking Get(int id);
        IEnumerable<QuizTaking> GetTakingsForQuizItem(int quizItemId);
        int Add(QuizTaking model);
        void Update(QuizTaking model);
        void Delete(int id);
    }
}