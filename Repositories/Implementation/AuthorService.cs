using MyBookStore.Models.Domain;
using MyBookStore.Repositories.Abstract;

namespace MyBookStore.Repositories.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext context;
        public AuthorService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Author model)
        {
            try
            {
                context.Author.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool Delete(int id)
        {
            var data = this.FindById(id);
            context.Author.Remove(data);
            context.SaveChanges();
            return true;
        }

        public Author FindById(int id)
        {
            return context.Author.Find(id);

        }

        public IEnumerable<Author> GetAll()
        {
            return context.Author.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                context.Author.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
