# PayrollApi
### This is a test task for OREL IT

Project configured with MS SQL database. Need to add Login=payroll; Password=payroll or change credentials in appsettings.json.\
Then need to make database migration, run command on project level:\
`dotnet ef database update`

API contains CRUD operations on payrolls and GetReport operation.
Full API description can be found on swagger page, which is launched in browser by default after project start.

To build docker image run following command on solution level:\
`docker build -t payroll_api .`

Then it can be used to rollout service in Kubernetes or any cloud service.