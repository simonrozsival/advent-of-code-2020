use std::io::prelude::*;
use std::io::{self, BufReader, Stdin};

fn read_input(input: Stdin) -> Vec<u64> {
    let reader = BufReader::new(input);
    reader.lines()
        .map(|line| line.unwrap())
        .map(|line| line.parse::<u64>().unwrap())
        .collect()
}

fn expected_sum_was_reached(expected_sum: u64) -> Option<Vec<u64>> {
    if expected_sum == 0 {
        Some(vec![])
    } else {
        None
    }
}

fn append(items: Vec<u64>, item: u64) -> Vec<u64> {
    let mut result = vec![item];
    result.extend_from_slice(&items);
    result
}

fn find_group_including(
    data: &Vec<u64>,
    number_in_group: u64,
    expected_sum: u64,
    item: u64
) -> Option<Vec<u64>> {
    find_group(&data, number_in_group - 1, expected_sum - item)
        .and_then(|items| Some(append(items, item)))
}

fn find_group(
    data: &Vec<u64>,
    number_in_group: u64,
    expected_sum: u64
) -> Option<Vec<u64>> {
    if number_in_group == 0 {
        expected_sum_was_reached(expected_sum)
    } else {
        data.iter()
            .filter(|i| **i <= expected_sum)
            .map(|i| find_group_including(&data, number_in_group, expected_sum, *i))
            .filter(|res| res.is_some())
            .next()
            .and_then(|res| res)
    }
}

fn print_result(items: Vec<u64>) {
    let decimal_repr: Vec<String> = items.iter().map(|u| u.to_string()).collect();
    let sum_str = decimal_repr.join(" + ");
    let product_str = decimal_repr.join(" * ");
    let sum: u64 = items.iter().sum();
    let product: u64 = items.iter().product();

    println!("Found result:");
    println!("{} = {}", sum_str, sum);
    println!("{} = {}", product_str, product);
}

fn main() {
    let data = read_input(io::stdin());
    let group_size = 3;
    let expected_sum = 2020;

    match find_group(&data, group_size, expected_sum) {
        Some(items) => print_result(items),
        None => println!("Could not find {} items which would sum to {}.", group_size, expected_sum)
    }
}
