# Scope & Requirements — Système de Notification ROBOCARE

**Projet** : ROBOCARE — Agriculture de précision (Tunisie)  
**Utilisateurs** : Agriculteurs, agronomes  
**Date** : Jour 1, Semaine 1, Mois 2

---

## 1. Canaux de notification

| Canal     | Outil / Provider        | Usage principal                  |
|-----------|-------------------------|----------------------------------|
| Email     | SendGrid / SMTP         | Rapports, confirmations          |
| SMS       | Twilio / infobip        | Alertes urgentes, accès limité   |
| WhatsApp  | Twilio / Meta API       | Alertes terrain, messages riches |
| Push      | Firebase FCM            | Notifications mobiles temps réel |

---

## 2. Cas d'usage principaux (5)

| # | Cas d'usage              | Description courte                                          | Canaux utilisés           |
|---|--------------------------|-------------------------------------------------------------|---------------------------|
| 1 | Alerte irrigation        | Seuil d'humidité sol dépassé → notifier l'agriculteur       | SMS, WhatsApp, Push       |
| 2 | Alerte météo             | Risque pluie/gel/vent fort prévu → avertissement préventif  | Email, SMS, Push          |
| 3 | Alerte ravageurs (pest)  | Détection de ravageurs sur capteur/image → action requise   | SMS, WhatsApp, Push       |
| 4 | Rapport hebdomadaire     | Résumé de la semaine (données, capteurs, santé cultures)    | Email                     |
| 5 | Confirmation d'action    | Accusé de réception d'une commande ou d'un arrosage planifié| Email, WhatsApp           |

---

## 3. Besoins fonctionnels

### API Endpoints

| Méthode | Endpoint                  | Description                            |
|---------|---------------------------|----------------------------------------|
| POST    | `/notifications`          | Créer et envoyer une notification      |
| GET     | `/notifications`          | Lister toutes les notifications        |
| GET     | `/notifications/{id}`     | Détail d'une notification              |
| GET     | `/notifications/status/{id}` | Statut d'envoi (PENDING/SENT/FAILED) |

### Templates

- Variables dynamiques supportées : `{{nom}}`, `{{champ}}`, `{{seuil}}`, `{{date}}`, `{{valeur}}`
- Un template par canal (email HTML, SMS texte court, WhatsApp message, Push titre+corps)
- Stockage en base de données (table `templates`)

### Logs

- Chaque tentative d'envoi est enregistrée (table `notification_logs`)
- Champs : `id`, `notification_id`, `canal`, `statut`, `erreur`, `timestamp`

---

## 4. Besoins non-fonctionnels

| Critère         | Valeur cible                                          |
|-----------------|-------------------------------------------------------|
| Volumétrie      | ≤ 500 notifications / minute (pic harvest season)    |
| Latence         | < 5 secondes pour canaux urgents (SMS, Push)          |
| Disponibilité   | SLA 99.5 % (hors maintenance planifiée)               |
| Retry           | 3 tentatives max avec backoff exponentiel             |
| Idempotence     | Clé d'idempotence pour éviter les doublons            |
| Traçabilité     | `correlation_id` sur chaque notification              |
| Sécurité        | Authentification JWT, HTTPS obligatoire               |
| Données PII     | Numéros de téléphone chiffrés en base                 |
