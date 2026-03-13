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
    // Ленивое создание последовательности чисел
    let numbersSeq = seq { for _ in 1 .. size -> readElement() }
    // Выводим элементы и их последние цифры сразу через map
    numbersSeq
    |> Seq.map (fun n ->
        let d = convertIntString n
        printfn "Элемент: %A" n
        printfn "Последняя цифра: %A" d
    )
// заставляет пройтись по всей последовательности
    |> Seq.toArray 

[<EntryPoint>]
let main args = 
    dialogueUser ()
    0