using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bridge.Core.Mapping;
using Bridge.Core.Models;
using Bridge.Core.Repository;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using NetTopologySuite.Geometries;
namespace Bridge.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper, EmailService emailService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _emailService = emailService;
        }

        public Task<Guid> Add(UserResource entity)
        {
            User user = _mapper.Map<User>(entity);
            return _userRepository.Add(user);
        }

        public async Task<IEnumerable<UserResource>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserResource>>(users);
        }

        public async Task<IEnumerable<UserResource>> GetUsersInRadius(double lat, double lng, double radiusInMeters)
        {
            var centerPoint = new Point(lng, lat)
            {
                SRID = 4326
            };
            var users = await _userRepository.GetUsersInRadius(centerPoint, radiusInMeters);
            return _mapper.Map<IEnumerable<UserResource>>(users);
        }
        public Task<int> DeleteById(Guid id)
        {
            return _userRepository.DeleteById(id);
        }

        public async Task<UserResource?> GetById(Guid id)
        {
            var user= await _userRepository.GetById(id);
            return _mapper.Map<UserResource?>(user);
        }

        public async Task<UserResource> LogIn(string email, string password)
        {
            User exitUser =await _userRepository.GetByEmail(email);
            if (exitUser == null)
            {
                return null;
            }
            bool isValid = _passwordHasher.Verify(password, exitUser.Password);
            if (!isValid)
                return null;
            return _mapper.Map<UserResource>(exitUser);
        }

        public async Task<UserResource> Register(UserRegisterResource registerResource)
        {
            var exitUser = await _userRepository.GetByEmail(registerResource.Email);
            if(exitUser!=null)
            {
                return null;
            }
            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = registerResource.Name,
                Email = registerResource.Email,
                Password = _passwordHasher.Hash(registerResource.Password),
                Phone=registerResource.Phone,
                Location= new Point(registerResource.Lng.Value, registerResource.Lat.Value)
                {
                    SRID = 4326
                }
            };

            await _userRepository.Add(newUser);
            try
            {
                await _emailService.SendWelcomeEmailAsync(newUser.Email, newUser.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmailService] שליחת מייל נכשלה: {ex.Message}");
            }
            return _mapper.Map<UserResource>(newUser);
        }

        public async Task<int> Update(UserResource userResource)
        {

            var userEntity = await _userRepository.GetById(userResource.Id);
            if (userEntity == null) return 0;
            _mapper.Map(userResource, userEntity);
            return await _userRepository.Update(userEntity);
        }

        public async Task<int> UpdateLocation(Guid userId, double lat, double lng)
        {
            var newLocation = new Point(lng, lat)
            {
                SRID = 4326
            };
            return await _userRepository.UpdateLocation(userId, newLocation);
        }

        public async Task<UserResource?> UpdateProfile(UpdateUserResource updateResource)
        {
            var userEntity = await _userRepository.GetById(updateResource.Id);
            if (userEntity == null) return null;

            userEntity.Name = updateResource.Name;
            userEntity.Phone = updateResource.Phone;
            await _userRepository.Update(userEntity);
            return _mapper.Map<UserResource>(userEntity);
        }
    }
}
