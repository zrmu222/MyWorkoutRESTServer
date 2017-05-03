using System;
using LibMyWorkout.Domain;
using System.Collections;

namespace myworkout.model.service.databaseService
{
    public interface IDatabaseService : IService
    {
        User getUser(string userName, string password);

        User saveNewUser(User user);

        User updateUser(User user);

        bool checkUserName(string userName);

        ArrayList getUsers();

    }


}
