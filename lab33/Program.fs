open System
open System.IO

// Функция для взаимодействия с пользователем и обработки ф
let dialogueUser () =
    // Запрос пути к каталогу
    let rec askDirectory () =
        printf "Введите путь к каталогу: "
        let dir = Console.ReadLine()
        if Directory.Exists dir then dir
        else
            printfn "Ошибка: указанный каталог не существует."
            askDirectory ()

    let dir = askDirectory ()

    // Функция для безопасного получения файлов из каталога
    let safeFiles path =
        try
            //перечисл коллекц полных имен ф
            Directory.EnumerateFiles(path)
        with
        //нет разрешения
        | :? UnauthorizedAccessException -> Seq.empty
        //путь-имя файла, ошибка ввода вывода
        | :? IOException -> Seq.empty

    // Функция для безопасного получения подкаталогов
    let safeDirectories path =
        try
            Directory.EnumerateDirectories(path)
        with
        | :? UnauthorizedAccessException -> Seq.empty
        | :? IOException -> Seq.empty

    // Рекурсивная функция для ленивого обхода всех файлов
    let rec getFiles (path: string) : seq<string> = seq {
        yield! safeFiles path
        for subdir in safeDirectories path do
            yield! getFiles subdir
    }
    // Получение ленивой последовательности всех файлов
    let allFiles = getFiles dir
    // Фильтрация файлов, исключая .txt
    let nonTextFiles =
        allFiles
        |> Seq.filter (fun file -> 
            Path.GetExtension(file).ToLower() <> ".txt")

    // Вывод результата
    nonTextFiles
    |> Seq.iter (printfn "%s")

[<EntryPoint>]
let main args =
    dialogueUser ()
    0