# Лабораторная работа 4

Реализовать программное средство, выполняющее вычисление и проверку электронной цифровой подписи (ЭЦП) текстового файла
на базе алгоритма DSA.
Для вычисления хеш-образа сообщения использовать функцию 3.2 из методических материалов (стр.22, Н0=100), вычисления
функции необходимо выполнять по модулю числа q.
Числа q, p, h, x и k ввести с клавиатуры.
Произвести все необходимые проверки для параметров, вводимых с клавиатуры.
В отдельное поле вывести полученный хеш сообщения в 10 с/cч.
ЭЦП вывести как два целых числа (если одно из полученных значений r или s будет равно 0, то необходимо повторить
вычисления).
Сформировать новое сообщение, состоящее из исходного сообщения и добавленной к нему цифровой подписи.
При проверке ЭЦП предусмотреть возможность выбора файла для проверки. На экран вывести результат проверки:

* сообщение о том, верна подпись или нет;
* вычисленные при проверке значения.

Для возведения в степень использовать быстрый алгоритм возведения в степень по модулю.
При нахождении обратного элемента s−1mod q или k−1 mod q использовать малую теорему Ферма в виде: s−1mod q = sq-2 mod q.
