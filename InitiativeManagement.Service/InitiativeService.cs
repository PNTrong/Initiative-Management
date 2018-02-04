using InitiativeManagement.Common;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InitiativeManagement.Service
{
    public interface IInitiativeService
    {
        bool Add(Initiative initiative, string userId);

        void Update(Initiative initiative);

        Initiative Delete(int id);

        IEnumerable<Initiative> GetAll();

        IEnumerable<Initiative> GetMulti();

        IEnumerable<Initiative> GetByIds(List<int> ids);

        IEnumerable<Initiative> GetAll(DynamicFilter fiter, out int totalCount, List<string> roles, string userId);

        IEnumerable<Initiative> GetAll(string keyword);

        IList<string> GetNames(string name);

        IEnumerable<Initiative> DownloadWord(DynamicFilter filter, List<string> roles, string userId);

        void Save();

        Initiative GetById(int id);

        Initiative FindById(int id);
    }

    public class InitiativeService : IInitiativeService
    {
        private IInitiativeRepository _initiativeRepository;
        private IUnitOfWork _unitOfWork;

        public InitiativeService(IInitiativeRepository initiativeRepository,
            IUnitOfWork unitOfWork)
        {
            _initiativeRepository = initiativeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// For Add initiative
        /// </summary>
        /// <param name="initiative"></param>
        /// <param name="userId">The account which added initiave</param>
        /// <param name="authors"></param>
        /// <returns></returns>
        public bool Add(Initiative initiative, string userId)
        {
            // the user which is the name of the initiative
            //
            if (string.IsNullOrEmpty(initiative.AccountId))
            {
                //
                // For User role
                initiative.AccountId = userId;
            } else
            {
                //
                // For Admin and SAdmin role
                initiative.AdminAccountId = userId;
            }

            initiative.IsDeactive = false;

            initiative.DateCreated = DateTime.Now;

            _initiativeRepository.Add(initiative);

            Save();

            return true;
        }

        public IList<string> GetNames(string name)
        {
            var result = new List<string>();

            var listNames = _initiativeRepository.GetMulti(_ => !_.IsDeactive).Select(item => item.Title).ToList();

            foreach (var item in listNames)
            {
                var resultCompare = Compute(name, item);

                if (resultCompare < 50)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public IEnumerable<Initiative> GetByIds(List<int> ids)
        {
            return _initiativeRepository.GetMulti(x => ids.Contains(x.Id), new string[] { "Field", "ApplicationUser" });
        }

        public Initiative Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Initiative FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Initiative> GetAll()
        {
            return _initiativeRepository.GetAll();
        }

        public IEnumerable<Initiative> GetAll(DynamicFilter filter, out int totalCount, List<string> roles, string userId)
        {
            IEnumerable<Initiative> query;

            // has permission to view all
            var keyword = filter.Keyword.ToLower();

            DateTime startTime;
            if (!DateTime.TryParse(filter.StartDate, out startTime))
            {
                // handle parse failure
                startTime = DateTime.MinValue;
            }

            DateTime endTime;
            if (!DateTime.TryParse(filter.EndDate, out endTime))
            {
                // handle parse failure
                endTime = DateTime.MaxValue;
            }

            if (roles.Any(x => x.Equals(Role.ViewIntiniativeForAdmin)))
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && DbFunctions.TruncateTime(x.DateCreated) >= startTime && DbFunctions.TruncateTime(x.DateCreated) <= endTime && x.AccountId.Contains(filter.AccountId)
                && (x.Title.ToLower().Contains(keyword)
                || x.KnowSolutionContent.ToLower().Contains(keyword)
                || x.ImprovedContent.ToLower().Contains(keyword)), new string[] { "Field", "ApplicationUser" });
            }
            else
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && x.AccountId == userId
                    && DbFunctions.TruncateTime(x.DateCreated) >= startTime && DbFunctions.TruncateTime(x.DateCreated) <= endTime
                    && (x.Title.ToLower().Contains(keyword)
                    || x.KnowSolutionContent.ToLower().Contains(keyword)
                    || x.ImprovedContent.ToLower().Contains(keyword)), new string[] { "Field", "ApplicationUser" });
            }

            if (filter.FieldId > -1)
            {
                query = query.Where(_ => _.FieldId == filter.FieldId);
            }

            if (filter.FieldGroupId > -1)
            {
                query = query.Where(_ => _.FieldGroupId == filter.FieldGroupId);
            }

            totalCount = query.Count();

            return query.OrderByDescending(x => x.DateCreated).Skip(filter.Skip).Take(filter.Take);
        }

        public IEnumerable<Initiative> DownloadWord(DynamicFilter filter, List<string> roles, string userId)
        {
            IEnumerable<Initiative> query = Enumerable.Empty<Initiative>();

            if (roles.Any(x => x.Equals(CommonConstants.ADMIN) || x.Equals(CommonConstants.ADVANCEDROLE)))
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive, new string[] { "Field" });
            }
            else
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && x.AccountId == userId, new string[] { "Field" });
            }

            //if (!string.IsNullOrEmpty(filter.Keyword))
            //    query = query.Where(x => x.Title.ToLower().Contains(filter.Keyword.ToLower()) || x.KnowSolutionContent.ToLower().Contains(filter.Keyword.ToLower()) ||
            //    x.ImprovedContent.ToLower().Contains(filter.Keyword.ToLower()));

            //if (!string.IsNullOrEmpty(filter.Time))
            //    query = query.Where(x => x.DeploymentTime.Year.ToString() == filter.Time);

            //if (filter.Field != null)
            //    query = query.Where(x => x.FieldId == filter.Field);

            return query.OrderBy(x => x.Title);
        }

        public IEnumerable<Initiative> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public Initiative GetById(int id)
        {
            return _initiativeRepository.GetSingleById(id);
        }

        public IEnumerable<Initiative> GetMulti()
        {
            return _initiativeRepository.GetMulti(item => !item.IsDeactive);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Initiative initiative)
        {
            _initiativeRepository.Update(initiative);
        }

        /// <summary>
        ///  Levenshtein distance computations
        /// </summary>
        /// <param name="s">the new value</param>
        /// <param name="t">the compare value</param>
        /// <returns></returns>
        private static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            var differenceValue = d[n, m];

            return differenceValue * 100 / m;
        }
    }
}