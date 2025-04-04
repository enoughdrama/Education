# 🏦 Тестирования банковского счета

## 📋 О работе
Демонстрирация применения автоматизированных модульных тестов для класса банковского счета с использованием средств Microsoft Visual Studio методом "белого ящика".

💻 Результаты работы приложения

![image](https://github.com/user-attachments/assets/e8117fd3-3f9e-49b9-a67c-613b666b75a9)
_Рис. 1. Результат работы консольного приложения BankAccount_

🧪 Результаты тестирования

![image](https://github.com/user-attachments/assets/78d9286b-79e5-474b-9afe-ae04f41be418)
_Рис. 2. Окно "Обозреватель тестов" с результатами тестирования_


## 🐛 Обнаруженные ошибки
В процессе тестирования была выявлена ошибка в методе Debit: вместо вычитания суммы с баланса происходило ее прибавление. Ошибка была устранена заменой оператора `+=` на `-=`.

## 🔧 Требуемый рефакторинг
В ходе тестирования код был улучшен:
- 📝 Добавлены константы для сообщений об ошибках
- 🛠️ Реализован более подробный конструктор исключений с указанием параметров
- 🔄 Успользованы блоки try/catch для более оптимизированного тестирования
