# ASP.NET Back-End Integration with Unity Game

## Overview

This repository contains a simulated version of a real-world back-end project developed using ASP.NET (Web API + MVC). The original project was tightly coupled with a Unity-based game (Animate-Free version), with which I was previously involved during its development phase.

Due to NDA restrictions, only a lightweight simulation is included here. The goal is to demonstrate the key challenges tackled and competencies applied during the actual development process.

## Role & Responsibilities

- Sole responsibility for analysis, architecture design, and development  
- Led the entire back-end system lifecycle, from planning to deployment

## 🏛 Architecture

- **Monolithic Onion Architecture**  
- **Shared physical directory** for optimal resource usage  
- Two main endpoints:  
  - Game API (Web API)  
  - Admin Panel (MVC)

### 🎮 Endpoint 1 – Game API

Connected to Unity game to handle:

- User profile creation  
- Authentication & JWT token generation  
- API Key system  
- SMS gateway integration  
- Custom user flow logic:
  - General game users
  - Game users participating in competitions (form submission required)
  - Direct competition participants  
- Significant challenge in modeling user entities and repository structure  
- File transfer using Microsoft's FormData, improving performance over previous Golang-based server

### ⚙️ Endpoint 2 – Admin Panel

- Built with ASP.NET MVC  
- Features include:
  - File upload management  
  - Reporting dashboard  
- Optimized for environments with extremely limited resources

## 🧱 Infrastructure Constraints

- Hosted on a single machine:
  - 2-Core CPU  
  - 4 GB RAM  
- SQL Server with minimal configuration  
- Shared physical directory used by both endpoints  
- Total users: 150,000  
- Peak competition load: 3,000 concurrent users

## 📦 Database Diagram

This diagram shows the core database structure used in the back-end system:

![Database Diagram](Panel\wwwroot/data.png)

## ⚠️ Disclaimer

This repository is a partial mock-up and does not contain the full production codebase due to confidentiality agreements.

## 🛠 Technologies Used

- ASP.NET Web API  
- ASP.NET MVC  
- SQL Server  
- Unity (client-side)  
- Microsoft FormData
