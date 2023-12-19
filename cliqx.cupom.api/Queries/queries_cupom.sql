--ALTER TABLE cupom MODIFY COLUMN ValorDesconto DECIMAL(10, 2);
--ALTER TABLE cupom MODIFY COLUMN PercentualDesconto DECIMAL(10, 2);

--alter table cupom_uso_pedido add column Cpf varchar(20) not null;

--ALTER TABLE cupom_uso_pedido 
--	ADD COLUMN ValorTotal DECIMAL(10, 2),
--	ADD COLUMN ValorTotalComDesconto DECIMAL(10, 2),
--    ADD COLUMN ValorCalculadoDesconto DECIMAL(10, 2)