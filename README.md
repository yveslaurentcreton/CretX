# CretX - The Action-Driven Proxy 🚀

**CretX** (pronounced *Cretix*) is a **smart reverse proxy** that doesn't just forward requests – it **automates actions** when traffic is received.  

Currently, CretX supports **Wake-on-LAN (WoL)** for a **single target machine**, allowing users to **wake up a system on demand** simply by navigating to a URL.  

Future updates will expand its **automation capabilities**, making it a **dynamic event-driven proxy** that can execute **custom rules** based on requests.

---

## 🌟 Current Features

✔ **Wake-on-LAN Proxy** – Automatically wakes up a **single machine** when accessed  
✔ **Intelligent Redirects** – Shows a **status page** while waiting for availability  
✔ **YARP-Based** – Uses **.NET's powerful reverse proxy technology**  

---

## 🚀 How It Works

1. The user navigates to **CretX** (e.g., `http://proxy.local/`)  
2. CretX **checks if the target machine is online**  
3. If offline, it **sends a Wake-on-LAN (WoL) packet**  
4. The user is temporarily redirected to a **status page**  
5. Once the machine is online, CretX **forwards traffic to the actual service**  

---

## ⚡ Roadmap & Future Vision

CretX aims to **become more than just a WoL proxy**. The goal is to **fully automate infrastructure actions based on request patterns**.

### 🔹 **Planned Features**

✅ **Multiple Wake-on-LAN Targets** – Configure multiple machines & wake **the right one**  
✅ **Configurable Automation Rules** – Define actions **per request type, endpoint, or user**  
✅ **Docker Support** – Deploy CretX in a **containerized environment**  
✅ **Service Auto-Shutdown** – Detect idle machines & **automatically shut them down**  
✅ **UI for Rule Management** – Easily configure **automation settings via a web interface**  

---

## 📦 Installation & Usage

⚠ **Docker support is planned but not yet available.**  
Currently, CretX runs as a **standalone .NET application**.

### **1️⃣ Clone the Repository**

```sh
git clone https://github.com/cretx/cretx.git
cd cretx
```

### **2️⃣ Configure Your Target Machine**

Edit `appsettings.json`

```json
{
  "CretX": {
    "TargetBaseUrl": "http://local-server:5000/",
    "MacAddress": "xx:xx:xx:xx:xx:xx"
  }
}
```

### **3️⃣ Run the Application**

```sh
dotnet run
```

## 📜 License
CretX is **open-source** under the **MIT License**.

**🚀 Get started with CretX today and bring automation to your proxy!**
