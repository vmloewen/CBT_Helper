using System;
using System.Collections.Generic;

namespace CBT_Helper
{
    public interface IRepository
    {

        Guid Create(ThoughtRecord thoughtRecord);

        IList<ThoughtRecord> GetAll();

        ThoughtRecord GetById(Guid guid);

        void Update(ThoughtRecord thoughtRecord);

        void DeleteById(Guid guid);

    }
}