open System

// Проверка корректного ввода размера последовательности
let rec readSize () = 
    printf "Введите кол-во элементов посл-ти: "
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

// Проверка корректного ввода элемента
let rec readElement () = 
    printf "Введите элемент: "
    let element = Console.ReadLine()
    try
        float element
    with
    | :? FormatException ->
        printfn "Ошибка: нужно ввести число."
        readElement ()

// Получение последней цифры
let convertIntString (x: float) = 
    let s = string x
    let s = s.Replace("-", "")
    let s = s.Replace(".", "")
    int (string s.[s.Length - 1])

let dialogueUser () = 
    let size = readSize()
    printfn "Введите элементы последовательности:"

    // Создаём строки сразу в одном ленивом проходе
    let numbersOutput, digitsOutput =
        seq { for _ in 1 .. size -> readElement() }
        |> Seq.fold (fun (nums, digs) n ->
            let d = convertIntString n
            (nums + sprintf "%A " n, digs + sprintf "%A " d)
        ) ("", "")

    printfn "Исходная последовательность: %s" numbersOutput
    printfn "Последовательность последних цифр: %s" digitsOutput

[<EntryPoint>]
let main args = 
    dialogueUser ()
    0