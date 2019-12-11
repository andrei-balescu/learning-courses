# Setting up Postgres on Ubuntu 18.4


1. Install Postgres and pgAdmin: `sudo apt-get install postgresql pgadmin3`
2. Create new Postgress role: `sudo -u postgres createuser --interactive`
3. Log in to postgres account: `sudo -u postgres psql`
4. Set password for new role: `\password "pgAdmin"`
5. Quit Postgres console: `\q`
5. Log in to role in pgAdmin client to manage server