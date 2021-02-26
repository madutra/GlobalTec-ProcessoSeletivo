----DESAFIO 2 MATHEUS ANDRADE DUTRA ------------

SELECT c1.Numero AS "Número do processo", p.Nome AS "Nome Fornecedor", c1.DataVencimento AS "Data de Vencimento",
	'Não pago' AS "Data de Pagamento", 
	c1.Valor AS "Valor Líquido", 'Contas a Pagar' AS "Identificador"
FROM `globaltec`.`contasapagar` c1
JOIN pessoas p ON c1.CodigoFornecedor = p.Codigo
UNION
SELECT c2.Numero AS "Número do processo", p.Nome AS "Nome Fornecedor", c2.DataVencimento AS "Data de Vencimento",
	c2.DataPagamento AS "Data de Pagamento",
	c2.Valor AS "Valor Líquido", 'Contas Pagas' AS "Identificador"
FROM `globaltec`.`contaspagas` c2
JOIN pessoas p ON c2.CodigoFornecedor = p.Codigo;

-------------------------------------------------