using System;
using Domain.Entites;

namespace Application.Interface;

public interface IRegisterRepository
{
    public bool AddUser(User userInfo);

    public User CheckEmail(string email);

    public bool AddSupportEngineer(SupportEngineer supportEngineer);
}

