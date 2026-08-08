# WPF ToDoList

Projet WPF simple de gestion de tâches (To‑Do list) développé en C#.

## Description
Application de bureau Windows permettant de créer, modifier, supprimer et marquer des tâches comme terminées. Interface réalisée en WPF, architecture recommandée : MVVM.

## Fonctionnalités
- Création, édition et suppression de tâches
- Marquage des tâches comme terminées / non terminées
- Filtrage simple des tâches (toutes / complétées / en cours)
- Persistance locale des données (fichier JSON ou équivalent)

## Technologies
- Plateforme : .NET Framework 4.8.1
- UI : WPF (XAML)
- Langage : C#

## Installation & exécution
Prérequis : Windows, .NET Framework 4.8.1, Visual Studio (workload .NET Desktop).

Depuis Visual Studio :
1. Ouvrir la solution `WpfApp.slnx` (racine du projet).
2. Restaurer les packages NuGet si nécessaire.
3. Construire la solution (Build) et lancer le projet principal.

## Organisation du code (suggestion)
- Models/ : classes de domaine (Task/ToDoItem)
- ViewModels/ : logique de présentation et commandes
- Views/ : XAML et code-behind
- Services/ : persistance (lecture/écriture JSON)

## Contribuer
- Forker le dépôt, créer une branche dédiée, ouvrir une pull request.

## Licence
À préciser (ex. MIT).

## Contact
Pour questions ou améliorations : voir le dépôt GitHub.
