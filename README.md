# Admin User information
UserName = "Admin"
Email = "Admin@TenderManagement.com"
Password = "Admin123!"

# ERD

```mermaid
erDiagram
    USER ||--o{ TENDER : owns
    USER ||--|| VENDOR : has

    VENDOR ||--o{ BID : submits
    TENDER ||--o{ BID : receives
    STATUS ||--o{ BID : tracks
    STATUS ||--o{ TENDER : tracks
    CATEGORY ||--o{ TENDER : categorizes

    USER {
        int Id PK
        string UserName
        string Email
        string PasswordHash
        int Role
        datetime CreatedAt
        datetime UpdatedAt  
    }

    VENDOR {
        int Id PK
        string Name
        string Address
        string Phone
        int UserId FK
        datetime CreatedAt
        datetime UpdatedAt  
    }

    CATEGORY {
        int Id PK
        string Name
        string Description
        datetime CreatedAt
        datetime UpdatedAt  
    }

    STATUS {
        int Id PK
        string Name
        string Description
        string Type
        datetime CreatedAt
        datetime UpdatedAt  
    }

    TENDER {
        int Id PK
        string Title
        string Description
        int CategoryId FK
        int OwnerId FK
        int StatusId FK
        datetime Deadline
        datetime CreatedAt
        datetime UpdatedAt  
    }

    BID {
        int Id PK
        decimal Amount
        string Comments
        int TenderId FK
        int VendorId FK
        int StatusId FK
        datetime SubmissionDate
        datetime CreatedAt
        datetime UpdatedAt  
    }
```
