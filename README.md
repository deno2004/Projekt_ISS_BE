# Dokumentacija projekta Task Tracker

## Opis projekta
Task Tracker je sistem za upravljanje nalog, ki omogoča sledenje nalog dodeljenih uporabnikom v okviru določenih projektov. Naloge so kategorizirane glede na status in imajo določene roke za izvedbo. Uporabniki imajo različne vloge in pravice glede na svoje dodeljene funkcije v sistemu.

## Struktura podatkovne baze

### 1. Users (Uporabniki)
| Polje | Tip podatka | Opis |
|-------|------------|------|
| id | INT, PRIMARY KEY, AUTO_INCREMENT | Enolični identifikator uporabnika |
| name | VARCHAR(100) | Ime uporabnika |
| email | VARCHAR(255), UNIQUE | E-poštni naslov uporabnika |
| role | VARCHAR(50) | Vloga uporabnika (npr. "Razvijalec", "Manager") |

### 2. Projects (Projekti)
| Polje | Tip podatka | Opis |
|-------|------------|------|
| id | INT, PRIMARY KEY, AUTO_INCREMENT | Enolični identifikator projekta |
| name | VARCHAR(255) | Ime projekta |
| description | TEXT | Opis projekta |
| start_date | DATE | Datum začetka projekta |
| end_date | DATE | Predviden zaključek projekta |

### 3. Tasks (Naloge)
| Polje | Tip podatka | Opis |
|-------|------------|------|
| id | INT, PRIMARY KEY, AUTO_INCREMENT | Enolični identifikator naloge |
| title | VARCHAR(255) | Naslov naloge |
| description | TEXT | Opis naloge |
| project_id | INT, FOREIGN KEY → Projects.id | Povezava s projektom |
| user_id | INT, FOREIGN KEY → Users.id | Uporabnik, ki mu je naloga dodeljena |
| status_id | INT, FOREIGN KEY → Status.id | Trenutni status naloge |
| deadline_id | INT, FOREIGN KEY → Deadlines.id | Rok za izvedbo naloge |

### 4. Status (Statusi)
| Polje | Tip podatka | Opis |
|-------|------------|------|
| id | INT, PRIMARY KEY, AUTO_INCREMENT | Enolični identifikator statusa |
| name | VARCHAR(50) | Naziv statusa (npr. "To-Do", "In Progress", "Completed") |

### 5. Deadlines (Roki)
| Polje | Tip podatka | Opis |
|-------|------------|------|
| id | INT, PRIMARY KEY, AUTO_INCREMENT | Enolični identifikator roka |
| task_id | INT, FOREIGN KEY → Tasks.id | Naloga, ki ji rok pripada |
| due_date | DATE | Datum roka |

## Funkcionalnosti sistema

1. **Dodajanje in urejanje projektov**  
   - Uporabniki lahko ustvarijo nove projekte.
   - Urejanje opisov, začetnih in končnih datumov projektov.

2. **Dodeljevanje nalog uporabnikom**  
   - Naloge se dodelijo specifičnim uporabnikom v okviru projekta.

3. **Spremljanje statusa nalog**  
   - Vsaka naloga ima status (npr. "To-Do", "In Progress", "Completed"), ki ga je mogoče posodabljati.

4. **Rok za izvedbo**  
   - Vsaka naloga ima določen rok za dokončanje.

5. **Seznam nalog po projektu**  
   - Prikaz vseh nalog, ki so povezane s posameznim projektom.

6. **Avtentikacija in avtorizacija (JWT Authentication)**  
   - Registracija in prijava uporabnikov.
   - Vloge uporabnikov določajo pravice dostopa do funkcionalnosti.

7. **Povezava s frontend aplikacijo (React + TypeScript)**  
   - API komunikacija med frontend in backend delom aplikacije.
   - React komponente za upravljanje projektov, nalog in uporabnikov.

## Tehnologije in orodja
- **Frontend**: React + TypeScript
- **Backend**: ASP.NET Core Web API
- **Podatkovna baza**: SQLite
- **Avtentikacija**: JWT (JSON Web Token)
- **Komunikacija**: REST API (Axios v frontend aplikaciji)

## Namestitev in zagon

### **Backend (ASP.NET Core)**
1. Klonirajte repozitorij in se pomaknite v mapo projekta.
2. Namestite potrebne pakete:
   ```sh
   dotnet restore
   ```
3. Ustvarite podatkovno bazo in izvedite migracije:
   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Zaženite aplikacijo:
   ```sh
   dotnet run
   ```

### **Frontend (React + TypeScript)**
1. Namestite pakete:
   ```sh
   npm install
   ```
2. Zaženite razvojni strežnik:
   ```sh
   npm start
   ```

## API endpointi

### **Avtentikacija**
- `POST /api/auth/register` – Registracija uporabnika
- `POST /api/auth/login` – Prijava uporabnika in pridobitev JWT

### **Projekti**
- `GET /api/projects` – Pridobi vse projekte
- `POST /api/projects` – Ustvari nov projekt
- `PUT /api/projects/{id}` – Posodobi projekt
- `DELETE /api/projects/{id}` – Izbriši projekt

### **Naloge**
- `GET /api/tasks` – Pridobi vse naloge
- `POST /api/tasks` – Ustvari novo nalogo
- `PUT /api/tasks/{id}` – Posodobi nalogo
- `DELETE /api/tasks/{id}` – Izbriši nalogo 🚀


