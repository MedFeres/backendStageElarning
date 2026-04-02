# Triggers — Système de Notification ROBOCARE

**Projet** : ROBOCARE — Agriculture de précision (Tunisie)  
**Utilisateurs** : Agriculteurs, agronomes  
**Date** : Jour 1, Semaine 1, Mois 2

---

## Tableau des triggers

| # | Trigger                  | Source                        | Canaux                  | Urgence  |
|---|--------------------------|-------------------------------|-------------------------|----------|
| 1 | Seuil humidité sol bas   | Capteur IoT irrigation        | SMS, WhatsApp, Push     | 🔴 Haute |
| 2 | Alerte météo extrême     | API météo externe             | Email, SMS, Push        | 🔴 Haute |
| 3 | Détection ravageurs      | Capteur / analyse image IA    | SMS, WhatsApp, Push     | 🟠 Moyen |
| 4 | Rapport hebdomadaire     | Tâche planifiée (cron)        | Email                   | 🟢 Basse |
| 5 | Confirmation arrosage    | Action utilisateur (app)      | Email, WhatsApp         | 🟢 Basse |
| 6 | Anomalie capteur          | Système de monitoring capteurs | Email, SMS              | 🟠 Moyen |

---

## Descriptions courtes

**1. Seuil humidité sol bas**  
Un capteur détecte que l'humidité du sol est en dessous du seuil configuré. L'agriculteur est averti immédiatement pour déclencher l'irrigation.

**2. Alerte météo extrême**  
Une prévision météo (gel, grêle, vent fort) est reçue via API externe. Une alerte préventive est envoyée avant l'événement.

**3. Détection ravageurs**  
Un capteur ou un système d'analyse image IA détecte la présence de ravageurs. L'agronome et l'agriculteur sont notifiés pour intervention.

**4. Rapport hebdomadaire**  
Chaque semaine (ex : lundi 7h), un résumé automatique (données capteurs, santé cultures, statistiques) est envoyé par email.

**5. Confirmation arrosage**  
Lorsque l'agriculteur programme un arrosage via l'application, un accusé de réception est envoyé par email ou WhatsApp.

**6. Anomalie capteur**  
Un capteur ne répond plus ou envoie des valeurs hors plage normale. L'équipe technique et l'agronome sont alertés pour maintenance.
