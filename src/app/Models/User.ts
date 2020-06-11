export class User{
    constructor(
 public Tz?: number,
 public Fullname?: string,
 public UserName?: string,
 public Birthdate?: Date,
 public Gender?: string,
 public Email?: string,
 public Password?: string,
 public Image?: any,
 public Permissions?: PermissionEnum
    ){}
} 


enum PermissionEnum{
    Manager= 1,
    Employee,
    SystemUser,
    Visitor,
}