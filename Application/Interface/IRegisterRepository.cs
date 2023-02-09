using System;
using Domain.DTO;
using Domain.Entites;

namespace Application.Interface;

public interface IRegisterRepository
{
    public bool AddUser(UserDTO userInfo);

    public User CheckEmail(string email);

    public bool AddSupportEngineer(SupportEngineer supportEngineer);
}

