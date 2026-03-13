open System

// Проверка корректного ввода размера последовательности
let rec readSize () = 
    printf "Введите кол-во элементов последовательности: "
    let size = Console.ReadLine()
    try
        let value = int size
        if value < 0 then
            printfn "Ошибка: число не может быть <0"
            readSize ()
        else
            value
    with
    | :? FormatException ->
        printfn "Ошибка: нужно ввести целое число."
        readSize ()

// Проверка ввода элемента (целое число)
let rec readElement () = 
    printf "Введите элемент (целое число): "
    let text = Console.ReadLine()
    try
        int text
    with
    | :? FormatException ->
        printfn "Ошибка: нужно ввести число."
        readElement ()

// Проверка ввода цифры (0–9)
let rec readDigit () = 
    printf "Введите цифру (0-9): "
    let text = Console.ReadLine()
    try
        let d = int text
        if d < 0 || d > 9 then
            printfn "Ошибка: введите цифру от 0 до 9."
            readDigit ()
        else
            d
    with
    | :? FormatException ->
        printfn "Ошибка: нужно ввести цифру."
        readDigit ()

// Проверяем, оканчивается ли число на заданную цифру
let endsWithDigit (x: int) (d: int) = 
    abs x % 10 = d

// Сумма элементов последовательности,
//оканчивающихся на заданную цифру
let sumByDigit sequence digit = 
    Seq.fold (fun acc x ->
        if endsWithDigit x digit then acc + x else acc
    ) 0 sequence

// Диалог с пользователем
let dialogueUser () =
    let size = readSize()
    printfn "Введите последовательность из %d элементов:" size
    let sequence = seq { for _ in 1 .. size -> readElement() }
    let digit = readDigit()
    let sum = sumByDigit sequence digit
    printfn "Сумма эл, оканчивающихся на %d = %d" digit sum

[<EntryPoint>]
let main args = 
    dialogueUser ()
    0