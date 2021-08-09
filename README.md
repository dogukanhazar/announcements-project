
# Announcements

🔔 This project is a web application for announcements in its simplest.



## Tech Stack

**Client:** Vue 3 with Vite, Vue router, Element Plus UI Library, Bootstrap 5, axios, moment

**Server:** .NET 5 WebAPI with Kestrel server

**Database:** Microsoft.EntityFrameworkCore.InMemory 

  
## Screenshots

![App Screenshot](https://i.ibb.co/WPyKxw3/image.png)
![App Screenshot](https://i.ibb.co/4TNsCDd/image.png)

  
## Run Locally

- sql database is empty at every startup because "in-memory database" is used
- api listening on: http://localhost:5000 and https://localhost:5001
- fe dev server: http://localhost:3000
Clone the project

```bash
  git clone https://github.com/dogukanhazar/announcements-project.git
```

Go to the project directory

```bash
  cd announcements-project
```

- #### Run Locally the Api Server

  Go to the api directory

  ```bash
    cd AnnouncementsApi
  ```

  Run dev server

  ```bash
    dotnet watch run
  ```

- #### Run Locally the Fe Server

  Go to the api directory

  ```bash
    cd AnnouncementsFe
  ```

  Install dependencies

  ```bash
    yarn
  ```

  Run dev server

  ```bash
    yarn dev
  ```

## API Reference

- For more details (swagger): https://localhost:5001/swagger/index.html 

#### Get items

```http
  GET /api/v1/Announcements
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `page` (query) | `int` | for pagination |
| `get_all` (query) | `boolean` | for get all items without pagination |

#### Get item

```http
  GET /api/v1/Announcements/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### Post item

```http
  POST /api/v1/Announcements
```

| Data | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `item`      | `object` | **Required**. {title:string, description:string, expiredDate:string} |

#### Put item - for update entire object

```http
  PUT /api/v1/Announcements/${id}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to put |

| Data | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `item`      | `object` | **Required**. {id:int, title:string, description:string, likes:int, createdDate:string, expiredDate:string} |

#### Patch item - to partially update the object

```http
  PATCH /api/v1/Announcements/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to patch |

| Data | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `parts`      | `array` | **Required**. [{op:string, path:string, value:any}] |

#### Delete item 

```http
  DELETE /api/v1/Announcements/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to delete |


  
  
## Enhancements

- sql database instead of in-memory database to backup data

- like and comment functionality

- admin panel

- new ui


  
## License

[MIT](https://choosealicense.com/licenses/mit/) @ Dogukan Hazar 2021

  