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

(*Создание последовательности
let createSeq size =
    seq {
        for i in 1 .. size do
            yield readElement()
    }
*)

// Преобразуем число в строку и берём последнюю цифру
let convertintString elements = 
    let s = string elements                 
    let s = s.Replace("-", "")  
    let s = s.Replace(".", "") 
    int (string s.[s.Length - 1])

let dialogueUser () = 
    let size = readSize()

    printfn "Введите элементы последовательности:"

    // один раз читаем элементы
    let buffer = Array.init size (fun _ -> readElement())
    // создаём последовательность
    let numbers = buffer |> Seq.ofArray
    let lastDigits = numbers |> Seq.map convertintString
    printf "Исходная последовательность: "
    numbers |> Seq.iter (printf "%A ")
    printfn ""
    printf "Последовательность последних цифр: "
    lastDigits |> Seq.iter (printf "%A ")

[<EntryPoint>]
let main args = 
    dialogueUser ()
    0