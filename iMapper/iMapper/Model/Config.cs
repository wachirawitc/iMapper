namespace iMapper.Model
{
    public class Config
    {
        public string ServerName { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public bool IsWindowsAuthentication { get; set; }

        public string KDiff { get; set; }

        public RepositoryConfig Repository { get; set; }

        public ModelConfig Model { get; set; }

        public ValidationConfig Validation { get; set; }

        public Config()
        {
            Repository = new RepositoryConfig();
            Model = new ModelConfig();
            Validation = new ValidationConfig();
        }
    }
}