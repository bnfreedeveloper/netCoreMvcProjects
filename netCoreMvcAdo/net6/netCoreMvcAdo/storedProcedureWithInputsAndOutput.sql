use StudentsAdo;
go
create procedure SP_UpdateStudent
@Id int,
@Name nvarchar(50),
@Email nvarchar(150),
@Result bit output
as 
begin
begin try
begin 
update Students
set Name =@Name, Email=@Email where id =@Id
select @Result = 1
End
end try

begin catch
select @Result = 0
end catch
end